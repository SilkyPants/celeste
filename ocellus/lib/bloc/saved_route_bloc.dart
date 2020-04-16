import 'dart:convert';
import 'dart:io';

import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter/services.dart' show rootBundle;
import 'package:path_provider/path_provider.dart';

import '../road_to_riches_response.dart';

@immutable
abstract class SavedRouteBlocEvent extends Equatable {
  @override
  List<Object> get props => [];
  SavedRouteBlocEvent() : super();
}

@immutable
abstract class SavedRouteBlocState extends Equatable {
  @override
  List<Object> get props => [];
  SavedRouteBlocState() : super();
}

/// Events

class LoadSavedRouteBlocEvent extends SavedRouteBlocEvent {
  @override
  List<Object> get props => [];

  LoadSavedRouteBlocEvent() : super();
}

class ToggleVisitedRouteBlocEvent extends SavedRouteBlocEvent {
  final Body body;
  @override
  List<Object> get props => [];

  ToggleVisitedRouteBlocEvent(this.body) : super();
}

/// States

class UninitialisedSavedRouteBlocState extends SavedRouteBlocState {}

class LoadingSavedRouteBlocState extends SavedRouteBlocState {}

class LoadedSavedRouteBlocState extends SavedRouteBlocState {
  final RoadToRichesRoute route;
  @override
  List<Object> get props => [route];
  LoadedSavedRouteBlocState({@required this.route}) : super();

  @override
  bool operator == (Object other) {
    return false;
  }

  @override
  int get hashCode => runtimeType.hashCode;
}

class ErrorSavedRouteBlocState extends SavedRouteBlocState {}

class SavedRouteBloc extends Bloc<SavedRouteBlocEvent, SavedRouteBlocState> {
  RoadToRichesRoute route;

  @override
  SavedRouteBlocState get initialState => UninitialisedSavedRouteBlocState();

  @override
  Stream<SavedRouteBlocState> mapEventToState(
      SavedRouteBlocEvent event) async* {
    if (event is LoadSavedRouteBlocEvent) {
      yield LoadingSavedRouteBlocState();
      await _loadRouteFromJson();
      yield LoadedSavedRouteBlocState(route: route);
    }

    if (event is ToggleVisitedRouteBlocEvent) {
      var body = route.stops
          .map((stop) => stop.bodies)
          .expand((i) => i)
          .toList()
          .firstWhere((body) => body.id == event.body.id);
      body.visited = !body.visited;
      _saveRouteToJson();
      yield LoadedSavedRouteBlocState(route: route);
    }
  }

  Future<String> loadAsset() async {
    return await rootBundle.loadString('assets/r2r.json');
  }



  Future<String> get _localPath async {
    final directory = await getApplicationDocumentsDirectory();
    return directory.path;
  }

  Future<File> get _localFile async {
    final path = await _localPath;
    return File('$path/savedRoute.json');
  }


  void _saveRouteToJson() async {
    try {
      final file = await _localFile;
      var contents = route.toJson();
      file.writeAsString(contents);
    } catch (e) {
      print("ERROR in Save!: Could not save integrations: $e");
    }
  }

  Future<bool> _loadRouteFromJson() async {
    try {
      final file = await _localFile;

      if (await file.exists()) {
        var contents = await file.readAsString();
        this.route = RoadToRichesRoute.fromJson(contents);
        print("Loaded route successfully");
        return true;
      }
      else {
        var json = await loadAsset();
        var routeResponse = RoadToRichesResponse.fromJson(json);
        this.route = RoadToRichesRoute(stops: routeResponse.result); 
        return true;
      }
    } catch (e) {
      print('ERROR in Load!: $e');
    }
    return false;
  }
}

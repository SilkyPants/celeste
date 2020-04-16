import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:intl/intl.dart';

import './bloc/saved_route_bloc.dart';
import './road_to_riches_response.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MultiBlocProvider(providers: [
      BlocProvider<SavedRouteBloc>(create: (_) => SavedRouteBloc()),
    ], child: buildMaterialApp());
  }

  MaterialApp buildMaterialApp() {
    return MaterialApp(
      title: 'Ocellus',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(title: 'Road to Riches'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  void initState() {
    super.initState();
    BlocProvider.of<SavedRouteBloc>(context).add(LoadSavedRouteBlocEvent());
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
      ),
      body: BlocBuilder(
          bloc: BlocProvider.of<SavedRouteBloc>(context),
          condition: (previousState, state) {
            // return true/false to determine whether or not
            // to rebuild the widget with state
            return true;
          },
          builder: (BuildContext context, SavedRouteBlocState state) {
            if (state is LoadingSavedRouteBlocState) {
              return _buildFetchingSpinner();
            }

            if (state is LoadedSavedRouteBlocState && state.route.stops.isNotEmpty) {
              return _buildRouteUI(route: state.route.stops);
            }

            return _buildEmptyState();
          }), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }

  Widget _buildFetchingSpinner() {
    return Center(
      child: CircularProgressIndicator(),
    );
  }

  Widget _buildListView(List<RoadToRichesStop> route) {
    return ListView.builder(
      scrollDirection: Axis.vertical,
      padding: EdgeInsets.all(4),
      itemCount: route.length,
      shrinkWrap: true,
      itemBuilder: (context, index) {
        if (index == 0) return Container();
        return RouteStopListItem(stop: route[index]);
      },
    );
  }

  Widget _buildEmptyState() {
    return Center(
      child: Text("No entries - sorry :("),
    );
  }

  Widget _buildRouteUI({List<RoadToRichesStop> route}) {
    var totalJumps = route.map((stop) => stop.jumps).reduce((value, element) => value + element);
    var totalMonies = route.map((stop) => stop.bodies)
    .reduce((value, element) => value + element)
    .map((body) => body.estimatedScanValue + body.estimatedMappingValue)
    .reduce((value, element) => value + element);

    var totalJumpsString = NumberFormat().format(totalJumps);
    var totalMoniesString = NumberFormat.compactCurrency(decimalDigits: 0, symbol: "").format(totalMonies);
    
    return Column(children: <Widget>[
      Container(
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Row(children: <Widget>[
            Expanded(child: Text("Total Jumps: $totalJumpsString")),
            Expanded(
              child: Text(
                'Total Credits: $totalMoniesString',
                textAlign: TextAlign.right,
              ),
            )
          ],),
        ),),
      Expanded(child: _buildListView(route),)
    ],);
  }
}

class RouteStopListItem extends StatelessWidget {
  const RouteStopListItem({
    Key key,
    @required this.stop,
  }) : super(key: key);

  final RoadToRichesStop stop;

  @override
  Widget build(BuildContext context) {
    return buildClassItemWidget(context);
  }

  Widget buildClassItemWidget(BuildContext context) {
    var bodyWidgets = stop.bodies.map((body) {
      return RouteStopBodiesListItem(body: body);
    }).toList();
    var titleWidget = StopTitleWidget(stop: stop);

    return Padding(
        padding: const EdgeInsets.all(8.0),
        child: ConstrainedBox(
            constraints: BoxConstraints(minHeight: 5),
            child: Column(
              children: <Widget>[titleWidget] + bodyWidgets,
            )));
  }
}

class StopTitleWidget extends StatelessWidget {
  const StopTitleWidget({
    Key key,
    @required this.stop,
  }) : super(key: key);

  final RoadToRichesStop stop;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Row(
        children: <Widget>[
          Expanded(child: Text(stop.name)),
          Expanded(
            child: Text(
              'Jumps: ${stop.jumps}',
              textAlign: TextAlign.right,
            ),
          )
        ],
      ),
    );
  }
}

class RouteStopBodiesListItem extends StatelessWidget {
  const RouteStopBodiesListItem({
    Key key,
    @required this.body,
  }) : super(key: key);

  final Body body;

  @override
  Widget build(BuildContext context) {

    var value = body.estimatedMappingValue + body.estimatedScanValue;
    var valueString = NumberFormat.compactCurrency(decimalDigits: 0, symbol: "").format(value);
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Row(children: <Widget>[
          Checkbox(
              value: body.visited,
              onChanged: (bool visited) {
                print("Toggle Visited State of ${body.name}");

                BlocProvider.of<SavedRouteBloc>(context)
                    .add(ToggleVisitedRouteBlocEvent(body));
              }),
          Expanded(
            child: Column(
              children: <Widget>[
                Row(
                  children: <Widget>[
                    Expanded(child: Text(body.name)),
                    Text(
                      'Distance: ${body.distanceToArrival} ls',
                      textAlign: TextAlign.right,
                    )
                  ],
                ),
                Container(
                  height: 8,
                ),
                Row(
                  children: <Widget>[
                    Expanded(child: Text('$valueString credits')),
                    Expanded(
                      child: Text(
                        body.subtype,
                        textAlign: TextAlign.right,
                      ),
                    )
                  ],
                )
              ],
            ),
          )
        ]),
      ),
    );
  }
}

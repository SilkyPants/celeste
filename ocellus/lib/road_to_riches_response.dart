// To parse this JSON data, do
//
//     final roadToRichesResponse = roadToRichesResponseFromJson(jsonString);

import 'dart:convert';

class RoadToRichesResponse {
    final List<RoadToRichesStop> result;
    final String status;

    RoadToRichesResponse({
        this.result,
        this.status,
    });

    factory RoadToRichesResponse.fromJson(String str) => RoadToRichesResponse.fromMap(json.decode(str));

    String toJson() => json.encode(toMap());

    factory RoadToRichesResponse.fromMap(Map<String, dynamic> json) => RoadToRichesResponse(
        result: List<RoadToRichesStop>.from(json["result"].map((x) => RoadToRichesStop.fromMap(x))),
        status: json["status"],
    );

    Map<String, dynamic> toMap() => {
        "result": List<dynamic>.from(result.map((x) => x.toMap())),
        "status": status,
    };
}

class RoadToRichesRoute {
    final List<RoadToRichesStop> stops;
    RoadToRichesRoute({
        this.stops,
    });

    factory RoadToRichesRoute.fromJson(String str) {
      var dict = json.decode(str);
      return RoadToRichesRoute.fromMap(dict);
    }

    String toJson() => json.encode(toMap());

    factory RoadToRichesRoute.fromMap(Map<String, dynamic> json) => RoadToRichesRoute(
        stops: List<RoadToRichesStop>.from(json["stops"].map((x) => RoadToRichesStop.fromMap(x))),
    );

    Map<String, dynamic> toMap() => {
        "stops": List<dynamic>.from(stops.map((x) => x.toMap())),
    };
}

class RoadToRichesStop {
    final List<Body> bodies;
    final int jumps;
    final String name;
    final double x;
    final double y;
    final double z;

    RoadToRichesStop({
        this.bodies,
        this.jumps,
        this.name,
        this.x,
        this.y,
        this.z,
    });

    factory RoadToRichesStop.fromJson(String str) => RoadToRichesStop.fromMap(json.decode(str));

    String toJson() => json.encode(toMap());

    factory RoadToRichesStop.fromMap(Map<String, dynamic> json) => RoadToRichesStop(
        bodies: List<Body>.from(json["bodies"].map((x) => Body.fromMap(x))),
        jumps: json["jumps"],
        name: json["name"],
        x: json["x"].toDouble(),
        y: json["y"].toDouble(),
        z: json["z"].toDouble(),
    );

    Map<String, dynamic> toMap() => {
        "bodies": List<dynamic>.from(bodies.map((x) => x.toMap())),
        "jumps": jumps,
        "name": name,
        "x": x,
        "y": y,
        "z": z,
    };
}

class Body {
    final int distanceToArrival;
    final int estimatedMappingValue;
    final int estimatedScanValue;
    final double id;
    final String id64;
    final bool isTerraformable;
    final String name;
    final String subtype;
    final String type;
    bool visited = false;

    Body({
        this.distanceToArrival,
        this.estimatedMappingValue,
        this.estimatedScanValue,
        this.id,
        this.id64,
        this.isTerraformable,
        this.name,
        this.subtype,
        this.type,
        this.visited,
    });

    factory Body.fromJson(String str) => Body.fromMap(json.decode(str));

    String toJson() => json.encode(toMap());

    factory Body.fromMap(Map<String, dynamic> json) => Body(
        distanceToArrival: json["distance_to_arrival"],
        estimatedMappingValue: json["estimated_mapping_value"],
        estimatedScanValue: json["estimated_scan_value"],
        id: json["id"].toDouble(),
        id64: json["id64"],
        isTerraformable: json["is_terraformable"],
        name: json["name"],
        subtype: json["subtype"],
        type: json["type"],
        visited: json["visited"] as bool ?? false,
    );

    Map<String, dynamic> toMap() => {
        "distance_to_arrival": distanceToArrival,
        "estimated_mapping_value": estimatedMappingValue,
        "estimated_scan_value": estimatedScanValue,
        "id": id,
        "id64": id64,
        "is_terraformable": isTerraformable,
        "name": name,
        "subtype": subtype,
        "type": type,
        "visited": visited,
    };
}

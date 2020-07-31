import { StarSystem } from "./star-system";

export class StarRoute {
  systems: StarSystem[];
  id: string;
  name: string;

  constructor(data: any) {
    this.systems = data["systems"] ?? [];
    this.id = data["id"] ?? "NO-ID";
    this.name = data["name"] ?? "Unknown Route";
  }

  get totalJumps(): number {
    return this.systems
      .map((system) => system.jumps)
      .reduce((result, value) => result + value);
  }

  get totalBodies(): number {
    return this.systems
      .map((system) => system.bodies)
      .reduce((result, value) => result.concat(value))
      .length;
  }

  get totalVisited(): number {
    return this.systems
      .map((system) => system.bodies.filter((body) => body.visited))
      .reduce((result, value) => result.concat(value))
      .length;
  }
}

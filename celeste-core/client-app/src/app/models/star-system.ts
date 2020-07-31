import { Body } from "./star-body";

export interface StarSystem {
  bodies: Body[];
  jumps: number;
  name: string;
  x: number;
  y: number;
  z: number;
}

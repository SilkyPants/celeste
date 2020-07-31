
export interface Body {
  distanceToArrival: number;
  estimatedMappingValue: number;
  estimatedScanValue: number;
  id: number;
  id64: string | null;
  isTerraformable: boolean;
  name: string;
  subtype: string;
  type: string | null;
  visited: boolean;
}

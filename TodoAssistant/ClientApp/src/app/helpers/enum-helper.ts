export function getEnumValues(enumType: any): number[] {
  return <number[]>Object.values(enumType).filter((r: any) => !isNaN(+r));
}

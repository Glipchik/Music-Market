export interface Claim {
  type: string;
  value: string;
}

export interface User {
  name: string;
  sub: string;
  claims: Claim[];
}

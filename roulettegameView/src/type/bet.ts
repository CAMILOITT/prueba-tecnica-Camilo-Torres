export enum TypeBet {
  red,
  black,
  odd,
  even,
  number,
}


export interface RequestBet {
  gain: number
  isWinner: boolean
  color: string
  number: number
}

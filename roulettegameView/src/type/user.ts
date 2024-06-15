export interface UserInformation {
  name: string
  amount: number
  gain: number
}

export interface UserInject {
  infoUser: UserInformation
  setName: (name: string) => void
  setMoney: (money: number) => void
}

<script setup lang="ts">
import { inject, reactive, ref } from 'vue'
import { RequestBet, TypeBet } from '../../type/bet'
import ItemBet from '../itemBet/ItemBet.vue'
import { UserInject } from '../../type/user';

const { infoUser } = inject('infoUser') as UserInject

const listColorMoney = [
  { color: '#F03A47', value: 5 },
  { color: '#2F195F', value: 10 },
  { color: '#F6F4F3', value: 15 },
  { color: '#276FBF', value: 20 }
]

const money = ref(listColorMoney[0].value)
const refDialogWinner = ref()
let resultBet = reactive<RequestBet>({
  gain: 0,
  isWinner: false,
  color: '',
  number: 0,
})

let listBet: { typeBet: TypeBet, value: string, amount: number }[] = []

function accBet(typeBet: TypeBet, value: string) {
  if (infoUser.amount < money.value) return
  const indexBet = listBet.findIndex(bet => bet.value === value)
  infoUser.amount = infoUser.amount - money.value
  if (indexBet < 0) {
    listBet.push({ typeBet, value, amount: money.value })
    return
  }
  listBet[indexBet].amount += money.value
}


async function sendBet() {
  console.log(JSON.stringify({ bet: listBet }));
  fetch('https://localhost:44342/api/Game', {
    method: 'POST',
    body: JSON.stringify({ bet: listBet }),
    headers: {
      'Content-Type': 'application/json'
    }
  }).then(res => res.json()).then((data: RequestBet) => {
    console.log(data);
    resultBet = data
    if (data.isWinner) {
      infoUser.gain = infoUser.gain + data.gain
      infoUser.amount = infoUser.amount + data.gain
    }
    refDialogWinner.value.showModal()
  }).catch(e => console.log(e))
}

function closeDialog() {
  (refDialogWinner.value as HTMLDialogElement).close()
}


</script>

<template>

  <dialog ref="refDialogWinner" class="dialog">
    <div v-if="resultBet.isWinner">
      <h1>GANASTE</h1>
      <p>{{ resultBet.gain }}</p>
      <h3>resultados</h3>
      <p>{{ resultBet.color }}</p>
      <button @click="closeDialog"> aceptar</button>
    </div>
    <div v-else>
      <h1>PERDISTE</h1>
      <h3>resultados</h3>
      <p>{{ resultBet.color }}</p>
      <button @click="closeDialog"> aceptar</button>
    </div>
  </dialog>

  <div class="bet">
    <div class="table-bet">
      <ItemBet class="item-bet item-zero" value="0" :type-bet="TypeBet.number" :amount="money" @set-value="accBet" />
      <div class="bet-number">
        <ItemBet class="item-bet" v-for="n in 36" :value="`${n}`" :type-bet="TypeBet.number" :amount="money"
          @set-value="accBet" />
      </div>
      <div class="bet-general">
        <ItemBet class="item-bet" value="par" :type-bet="TypeBet.even" :amount="money" @set-value="accBet" />
        <ItemBet class="item-bet" value="red" :type-bet="TypeBet.red" :amount="money" @set-value="accBet" />
        <ItemBet class="item-bet" value="black" :type-bet="TypeBet.black" :amount="money" @set-value="accBet" />
        <ItemBet class="item-bet" value="impar" :type-bet="TypeBet.odd" :amount="money" @set-value="accBet" />
      </div>
    </div>
    <div class="money">
      <div class="currency" :style="{ background: color }" v-for="({ color, value }) in listColorMoney"
        :class="{ selected: money === value }" @click="money = value"> {{ value }} </div>
      <button @click="sendBet">apostar</button>
    </div>
  </div>
</template>

<style scoped>
.dialog {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 1rem;
  border-radius: 1rem;
}

.bet {
  position: absolute;
  bottom: 5px;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.selected {
  outline: 3px solid white
}

.table-bet {
  display: grid;
  grid-template-columns: 3rem 1fr;
  width: 100dvw;

  .item-zero {
    grid-row-start: 1;
    grid-row-end: 3;
    background: black
  }

  .item-bet {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    outline: 2px solid white;
    font-size: 1.5rem;
    font-weight: bold
  }


  .bet-number {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(4rem, 1fr));

    .item-bet {
      --size: 4rem;
      width: var(--size);
      height: var(--size);
      background: black;

      &:nth-child(even) {
        background: red;
      }
    }
  }

  .bet-general {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(2rem, 1fr));

    .item-bet {
      font-size: 2rem;
    }
  }

}

.money {
  display: flex;
  gap: .5rem;
  justify-content: space-evenly;
  align-items: center;

  .currency {
    --size: 90px;
    width: var(--size);
    height: var(--size);
    color: black;
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
    transition: 100ms;
    position: relative;
    z-index: 1;
    font-weight: bold;
    font-size: 2rem;

    &:hover {
      scale: 1.01;
    }

    &::before {
      content: '';
      --size: 80%;
      width: var(--size);
      height: var(--size);
      position: absolute;
      top: 50%;
      left: 50%;
      translate: -50% -50%;
      background: white;
      border-radius: 50%;
      z-index: -1;
    }
  }

  &>button {
    padding: .5rem 1rem;
    background: goldenrod;
    font-weight: bold;
  }
}
</style>

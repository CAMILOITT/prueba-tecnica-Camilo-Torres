<script setup lang="ts">
import { ref } from 'vue'
import { TypeBet } from '../../type/bet'
import ItemBet from '../itemBet/ItemBet.vue'

const listColorMoney = [
  { color: '#F03A47', value: 5 },
  { color: '#2F195F', value: 10 },
  { color: '#F6F4F3', value: 15 },
  { color: '#276FBF', value: 20 }
]

const money = ref(0)


// function bet(typeBet, value, amountAcc) {
//   console.log(typeBet, value, amountAcc)
// }


</script>

<template>
  <div class="bet">
    <div class="table-bet">
      <ItemBet class="item-bet item-zero" value="0" :type-bet="TypeBet.number" :amount="money" />
      <div class="bet-number">
        <ItemBet class="item-bet" v-for="n in 36" :value="`${n}`" :type-bet="TypeBet.number" :amount="money" />
      </div>
      <div class="bet-general">
        <ItemBet class="item-bet" value="par" :type-bet="TypeBet.even" :amount="money" />
        <ItemBet class="item-bet" value="red" :type-bet="TypeBet.red" :amount="money" />
        <ItemBet class="item-bet" value="black" :type-bet="TypeBet.black" :amount="money" />
        <ItemBet class="item-bet" value="impar" :type-bet="TypeBet.odd" :amount="money" />
      </div>
    </div>
    <div class="money">
      <div class="currency" :style="{ background: color }" v-for="({ color, value }) in listColorMoney"
        @click="money = value"> {{ value }}</div>
      <button>apostar</button>
    </div>
  </div>
</template>

<style scoped>
.bet {
  position: absolute;
  bottom: 5px;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
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
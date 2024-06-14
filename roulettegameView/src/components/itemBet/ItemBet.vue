<script setup lang="ts">
import { TypeBet } from '../../type/bet'
import { ref } from 'vue'

interface Props {
  typeBet: TypeBet
  value: string
  amount: number
}

const { typeBet, value, amount } = defineProps<Props>()
const emit = defineEmits(['setValues'])
const amountAcc = ref(0)

function addAmount() {
  amountAcc.value = amountAcc.value + amount
  console.log({ amount, amountAcc: amountAcc.value })
  emit('setValues', typeBet, value, amountAcc)
}


</script>

<template>
  <div @click="addAmount" class="item">
    {{ value }}
  </div>
</template>

<style scoped>
.item{
  cursor:default;
  
}
</style>
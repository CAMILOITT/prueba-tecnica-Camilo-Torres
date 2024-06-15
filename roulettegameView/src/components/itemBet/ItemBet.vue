<script setup lang="ts">
import { TypeBet } from '../../type/bet'

interface Props {
  typeBet: TypeBet
  value: string
}

const { typeBet, value, } = defineProps<Props>()

</script>

<template>
  <div @click="$emit('setValue', typeBet, value)" class="item">
    {{ value }}
  </div>
</template>

<style scoped>
.item {
  cursor: default;
}
</style>
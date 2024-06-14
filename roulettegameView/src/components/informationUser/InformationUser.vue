<script setup lang="ts">

interface Props {
  user: string
  money: number
}

defineProps<Props>()
</script>

<template>
  <div class="info-player">
    <h3>{{ user }}</h3>
    <span>$ {{ money }}</span>
  </div>
</template>

<style>
.info-player {
  background: red;
  position: absolute;
  top: 10px;
  left: 20px;
  padding: .5rem 1rem;
  border-radius: 1rem;
}
</style>
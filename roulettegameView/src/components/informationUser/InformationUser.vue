<script setup lang="ts">
import { inject } from 'vue';
import { UserInject } from '../../type/user';



const { infoUser } = inject('infoUser') as UserInject
</script>

<template>
  <div class="info-player">
    <h3>{{ infoUser.name }}</h3>
    <span>$ {{ infoUser.amount }}</span>
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
<script lang="ts" setup>
import { reactive,toRefs,ref } from 'vue'
import doctors from './doctors.vue'
import recoverystory from './recoverystory.vue'
</script>
<template>
      <el-affix :offset="200">
            <router-link to="/reservationdoctor" target="_blank" style="float:right">预约
            </router-link>
         </el-affix>
    <doctors/>
    <recoverystory/>
</template>
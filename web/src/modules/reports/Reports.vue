<template>
  <div class>
    <h2>Berichte</h2>
    <Sales/>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import Sales from './components/Sales.vue'

const Reports = Vue.extend({
  components: {
    Sales
  }
})

export default Reports
</script>

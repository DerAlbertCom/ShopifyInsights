<template>
  <el-container>
    <el-header>
      <Header />
    </el-header>
    <el-container>
      <el-aside width="13em">
        <router-view name="aside" />
      </el-aside>
      <el-main><router-view /></el-main>
    </el-container>
    <el-footer><Footer /></el-footer>
  </el-container>
</template>

<script lang="ts">
import Vue from 'vue'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

const Layout = Vue.extend({
  name: 'Layout',
  components: {
    Header,
    Footer
  }
})
export default Layout
</script>

<style scoped>
  .el-aside {
    background-color: #42b983;
  }

  .el-header {
    border-bottom: solid 1px #e6e6e6;
  }
</style>

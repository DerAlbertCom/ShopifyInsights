<template>
  <el-menu
    mode="horizontal"
    :router="true"
  >
    <el-menu-item>
      <router-link to="/">
        <img
          height="50px"
          src="https://cdn.shopify.com/s/files/1/0034/4086/9476/files/Logo_Vektor_InDesign_360x.jpg?v=1547415650"
        >
      </router-link>
    </el-menu-item>
    <el-menu-item index="reports">
      Berichte
    </el-menu-item>
    <el-menu-item index="orders">
      Bestellungen
    </el-menu-item>
    <el-menu-item index="customers">
      Kunden
    </el-menu-item>
  </el-menu>
</template>

<script lang="ts">

export default {
  name: 'Header'
}

</script>

<style scoped lang="scss">
  .el-menu.el-menu--horizontal {
    border-bottom: transparent;
  }
</style>

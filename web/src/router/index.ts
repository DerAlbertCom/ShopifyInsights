import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Layout from '../views/Layout.vue'
import Home from '../views/Home.vue'
import ReportsRoute from '../modules/reports/routes';
import OrdersRoute from '../modules/orders/routes';
import CustomersRoute from '../modules/customers/routes';

Vue.use(VueRouter)

const routes: RouteConfig[] = [
  {
    path: '/',
    name: 'homeContainer',
    redirect: 'home',
    component: Layout,
    meta: {
      title: 'Home'
    },
    children: [
      {
        path: '',
        name: 'home',
        component: Home,
        meta: {
          title: 'Home',
          hide: true
        }
      },
      ReportsRoute, OrdersRoute, CustomersRoute
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router

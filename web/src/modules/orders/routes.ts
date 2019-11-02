import { RouteConfig } from 'vue-router';

const routes: RouteConfig = {
  path: '/orders',
  name: 'ordersContainer',
  redirect: 'orders',
  components: {
    default: () => import(/* webpackChunkName: "Orders" */ './OrdersContainer.vue'),
    aside: () => import(/* webpackChunkName: "Orders" */ './OrdersAside.vue')
  },
  meta: {
    title: 'Bestellungen'
  },
  children: [
    {
      path: '',
      name: 'orders',
      component: () => import(/* webpackChunkName: "Orders" */ './Orders.vue')
    }
  ]
}

export default routes

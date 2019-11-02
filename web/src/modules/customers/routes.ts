import { RouteConfig } from 'vue-router';

const routes: RouteConfig = {
  path: '/customers',
  name: 'customersContainer',
  redirect: 'customers',
  components: {
    default: () => import(/* webpackChunkName: "Customers" */ './CustomersContainer.vue'),
    aside: () => import(/* webpackChunkName: "Customers" */ './CustomersAside.vue')
  },
  meta: {
    title: 'Kunden'
  },
  children: [
    {
      path: '',
      name: 'customers',
      component: () => import(/* webpackChunkName: "Customers" */ './Customers.vue')
    }
  ]
}

export default routes

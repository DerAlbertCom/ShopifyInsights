import { RouteConfig } from 'vue-router';

const routes: RouteConfig = {
  path: '/reports',
  name: 'reportsContainer',
  redirect: 'reports',
  components: {
    default: () => import(/* webpackChunkName: "Reports" */ './ReportsContainer.vue'),
    aside: () => import(/* webpackChunkName: "Reports" */ './ReportsAside.vue')
  },
  meta: {
    title: 'Berichte'
  },
  children: [
    {
      path: '',
      name: 'reports',
      component: () => import(/* webpackChunkName: "Reports" */ './Reports.vue')
    }
  ]
}

export default routes

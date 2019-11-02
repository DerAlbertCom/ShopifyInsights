import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueCompositionApi from '@vue/composition-api'
import './plugins/element.js'
import VueLayers from 'vuelayers'

Vue.config.productionTip = false
Vue.use(VueCompositionApi)
Vue.use(VueLayers, {
  dataProjection: 'EPSG:4326'
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

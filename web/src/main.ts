import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueCompositionApi from '@vue/composition-api'
import './plugins/element.js'
import 'element-ui/lib/theme-chalk/index.css'
// @ts-ignore
import locale from 'element-ui/lib/locale/lang/de'
import VueLayers from 'vuelayers'

Vue.config.productionTip = false
Vue.use(VueCompositionApi, locale)
Vue.use(VueLayers, {
  dataProjection: 'EPSG:4326'
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

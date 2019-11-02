import Vue from 'vue'

import { Line, mixins } from 'vue-chartjs'
import { ChartOptions } from 'chart.js'
const { reactiveProp } = mixins

const LineChart = Vue.extend({
  extends: Line,
  mixins: [reactiveProp],
  props: {
    options: {
      type: Object,
      default: null
    }
  },
  mounted () {
    // this.chartData is created in the mixin.
    // If you want to pass options please create a local options object
    // @ts-ignore
    this.renderChart(this.chartData, this.options)
  }
})

export default LineChart

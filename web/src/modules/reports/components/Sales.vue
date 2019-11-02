<template>
  <div>
    <h3>Umsätze</h3>
    <LineChart
      :styles="styles"
      :chart-data="dataCollection"
      :options="chartOptions"
    />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import LineChart from './LineChart'
import { ChartData, ChartOptions } from 'chart.js';
type IData = {
  dataCollection: ChartData,
  chartOptions: ChartOptions,
  height: number
}
const Sales = Vue.extend({
  name: 'Sales',
  components: {
    LineChart
  },
  data () : IData {
    return {
      dataCollection: {},
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
        aspectRatio: 1.6,
        scales: {
          yAxes: [{
            ticks: {
              beginAtZero: true
            }
          }]
        }
      },
      height: 600
    }
  },
  computed: {
    styles () {
      return {
        // @ts-ignore
        height: `${this.height}px`,
        position: 'relative'
      }
    }
  },
  async mounted () {
    await this.fetchData()
  },
  methods: {
    async fetchData () {
      const url = `https://localhost:5001/api/reports/sales?from=2019-01-01&to=2019-11-02&location=all`;
      const response = await fetch(url);
      const result = await response.json();
      this.dataCollection = {
        labels: result.labels,
        datasets: [{
          label: 'Umsatz',
          data: result.data,
          backgroundColor: [
            'rgba(255, 99, 132, 0.2)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)'
          ],
          borderWidth: 1
        }]
      }
    }
  }
})
export default Sales
</script>

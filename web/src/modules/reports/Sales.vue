<template>
  <div>
    <h2>Verkäufe</h2>
    <form>
      <div class="block">
        <span class="demonstration">Zeitraum</span>
        <el-date-picker
          v-model="selectedDates"
          type="daterange"
          range-separator="bis"
          start-placeholder="Begin-Datum"
          end-placeholder="End-Datum"
        />
        <el-button
          :disabled="cantShow"
          @click="loadData"
        >
          Anzeigen
        </el-button>
      </div>
    </form>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import fetchData from '@/services/webService';

type Data = {
  selectedDates: string[]
  salesData : SalesData | null
}

type SalesData = {
}

const Sales = Vue.extend({
  components: {},
  data () : Data {
    return {
      selectedDates: [],
      salesData: null
    }
  },
  computed: {
    localeDate () : string[] {
      if (this.selectedDates.length === 2) {
        return [getLocaleIsoDate(this.selectedDates[0]), getLocaleIsoDate(this.selectedDates[1])]
      }
      return []
    },
    cantShow () : boolean {
      return this.selectedDates.length !== 2;
    }
  },
  mounted () {
  },
  methods: {
    async loadData () {
      return fetchData<SalesData>(`api/report/salesfordate&from=${this.localeDate[0]}&to=${this.localeDate[1]}`)
    }
  }
})

function getLocaleIsoDate (dateString: string) {
  let date = new Date(dateString)
  const z = date.getTimezoneOffset() * 60 * 1000;
  // @ts-ignore
  const dateLocal = date - z;
  date = new Date(dateLocal);
  return date.toISOString().substring(0, 10);
}
export default Sales
</script>

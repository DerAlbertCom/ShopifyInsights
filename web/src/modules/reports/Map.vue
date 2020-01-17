<template>
  <div>
    <h2>Karte</h2>
    <vl-map
      :load-tiles-while-animating="true"
      :load-tiles-while-interacting="true"
      style="height: 500px"
      @click="layerClicked"
    >
      <vl-view
        :zoom.sync="zoom"
        :center.sync="center"
        :rotation.sync="rotation"
      />

      <vl-layer-tile id="osm">
        <vl-source-osm url="" />
      </vl-layer-tile>

      <vl-layer-vector>
        <vl-source-vector
          :features.sync="features"
        />
      </vl-layer-vector>
    </vl-map>
    <p v-if="loading">
      Loading features, please wait...
    </p>
    Center {{ center }} Zoom: {{ zoom }}
  </div>
</template>
<script lang="ts">
import Vue from 'vue'

type data = {
  features: any[],
  loading: boolean,
  zoom: number,
  center: number[],
  rotation: number
}
const Reports = Vue.extend({
  components: {},
  data(): data {
    return {
      zoom: 5,
      center: [11, 50],
      rotation: 0,
      features: [],
      loading: false
    }
  },
  async mounted() {
    this.loading = true;
    const features = await this.loadFeatures();
    this.features = features.map(Object.freeze);
    this.loading = false
  },
  methods: {
    // emulates external source
    async loadFeatures(): Promise<any[]> {
      const response = await fetch('https://localhost:5001/api/maps/sales');
      return response.json();
    },
    layerClicked(a: any, b: any) {
      console.debug('clicked', a, b);
      alert(a);
    }
  }
});

export default Reports

</script>

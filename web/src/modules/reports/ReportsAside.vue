<template>
  <ul>
    <li>
      <router-link :to="{ name: 'reports' }">
        Umsatz
      </router-link>
    </li>
    <li>
      <router-link :to="{ name: 'reports-map' }">
        Karte
      </router-link>
    </li>
    <li>
      <router-link :to="{ name: 'reports-sales' }">
        Verkäufe
      </router-link>
    </li>
  </ul>
</template>

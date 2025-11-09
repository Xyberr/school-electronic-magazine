import { createApp } from 'vue'
import type { Plugin } from 'vue'
import PrimeVueConfig from 'primevue/config'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'

import App from './App.vue'
import router from './router/index.ts'
import { definePreset } from '@primevue/themes'

const app = createApp(App)

const MyPreset = definePreset(Aura, {
  semantic: {
    primary: {
      50: '{indigo.50}',
      100: '{indigo.100}',
      200: '{indigo.200}',
      300: '{indigo.300}',
      400: '{indigo.400}',
      500: '{indigo.500}',
      600: '{indigo.600}',
      700: '{indigo.700}',
      800: '{indigo.800}',
      900: '{indigo.900}',
      950: '{indigo.950}',
    },
  },
})

app.use(router)

app.use(PrimeVueConfig as unknown as Plugin, {
  theme: {
    preset: MyPreset,
    options: {
      darkModeSelector: false,
    },
  },
})

app.mount('#app')

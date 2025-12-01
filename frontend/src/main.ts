import { createApp } from 'vue'
import PrimeVue from 'primevue/config'

import App from './App.vue'
import router from './router/index'
import { MyPreset } from './primevue-styles'

const app = createApp(App)

app.use(router)

app.use(PrimeVue, {
  theme: {
    preset: MyPreset,
    options: {
      darkModeSelector: false,
    },
  },
})

app.mount('#app')

import { createApp } from 'vue'
import PrimeVue from 'primevue/config'

import App from './App.vue'
import router from './router/index'
import { MyPreset } from './primevue-styles'
import ToastService from 'primevue/toastservice'

const app = createApp(App)

app.use(router)
app.use(ToastService)

app.use(PrimeVue, {
  theme: {
    preset: MyPreset,
    options: {
      darkModeSelector: false,
    },
  },
})

app.mount('#app')

import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 3000,
    proxy: {
      "^/api": {
        target: "http://101.42.19.77:6001/",
        changeOrigin: true,
        //rewrite: (path) => path.replace(/^\/api/, ""),
      },
    },
  },
})

import { createStore } from 'vuex';

const store = createStore({
  state: {
    token: null,
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
    },
    clearToken(state) {
      state.token = null;
    },
  },
  actions: {
    saveToken({ commit }, token) {
      // Save the token to localStorage and Vuex state
      localStorage.setItem('token', token);
      commit('setToken', token);
    },
    logout({ commit }) {
      // Clear the token from localStorage and Vuex state
      localStorage.removeItem('token');
      commit('clearToken');
    },
  },
});

export default store;

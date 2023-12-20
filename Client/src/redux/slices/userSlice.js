/* eslint-disable no-param-reassign */

import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  authInfo: localStorage.getItem('authInfo') ? JSON.parse(localStorage.getItem('authInfo')) : null,
  loggedIn: localStorage.getItem('loggedIn') ? JSON.parse(localStorage.getItem('loggedIn')) : false,
  userInfo: localStorage.getItem('userInfo') ? JSON.parse(localStorage.getItem('userInfo')) : null,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setAuthToken: (state, action) => {
      state.loggedIn = true;
      state.authInfo = action.payload;
      localStorage.setItem('loggedIn', JSON.stringify(true));
      localStorage.setItem('authInfo', JSON.stringify(action.payload));
    },
    unsetAuthToken: (state) => {
      state.authInfo = null;
      state.loggedIn = false;
      state.userInfo = null;
      localStorage.clear();
    },
  },
});

export const { setAuthToken, unsetAuthToken } = userSlice.actions;
export default userSlice.reducer;

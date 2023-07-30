<script setup>
import { ref, watch , reactive} from 'vue';
import axios from "axios";
import jsonData from './values.json';
import background from './login-register.vue'

const provinces = ref(jsonData.provinces);
const selectedProvince = ref('');
const cities = ref([]);
const selectedCity = ref('');
const phone= ref('');
const phoneError = ref(false);
const username= ref('');
const password= ref('');
const confirmPassword= ref('');
const passwordError = ref(false);
const passwordTooShort = ref(false);
const passwordTooLong = ref(false);
const passwordMinLength = 6;
const passwordMaxLength = 20;

const validatePasswordLength = () => {
  const passwordLength = password.value.length;
  passwordTooShort.value = passwordLength < passwordMinLength;
  passwordTooLong.value = passwordLength > passwordMaxLength;
};


watch(selectedProvince, (newSelectedProvince) => {
  cities.value = Object.entries(jsonData[newSelectedProvince] || {}).map(([key, value]) => ({
    key,
    value
  }));
});

const validatePhoneNumber = () => {
  const phoneNumberPattern = /^1\d{10}$/;
  phoneError.value = !phoneNumberPattern.test(phone.value);
};

const validconfirmPassword = () => {
  if(password.value!=confirmPassword.value)
    passwordError.value = true;
  else
    passwordError.value = false;
}

const submitForm = (event) => {
    event.preventDefault();
    const data = {
        phone: phone.value,
        region: selectedCity.value,
        username: username.value,
        password: password.value
    };
    axios
        .post('/api/register', data)
        .then(response => {
          // 处理响应
          alert(`Hello ${username.value}!，注册成功！`);
        })
        .catch(error => {
          alert('注册失败！');
          console.error('请求出错:', error);
        });
};

</script>


<template>
  <background></background>
	<div class="form-container">
	  <div class="left-half">
	    <h1>注册信息</h1>
	    <div class="form-group">
        <div class="phone-input">
	        <label for="phone">您的电话号码</label>
          <span v-if="!phoneError && phone" class="success-icon fas fa-check-circle" style="color: green;
          position: absolute;
          margin-left: 35%;"></span>
          <span v-if="phoneError && phone" class="error-phone">您的号码格式不正确</span>
        </div>
          <input type="text" v-model="phone" name="phone" placeholder="请输入电话" @input="validatePhoneNumber"/>
      </div>

	    <div class="form-group">
	      <label for="region">选择地区</label>

	      <select v-model="selectedProvince" class="province-select">
	        <option v-for="(key,value) in provinces" :value="key" :key="key">{{ value }}</option>
	      </select>
        <select v-model="selectedCity" name="cities" class="city-select">
            <option v-for="city in cities" :value="city.key" :key="city.key">{{ city.key }}</option>
        </select>
	    </div>
	  </div>
	  <div class="right-half">
	    <form>
	      <div class="form-group">
	        <label for="username">填写用户名</label>
	        <input type="text" v-model="username" name="username" placeholder="请输入账号" />
	      </div>
	      <div class="form-group">
          <div class="password-input">
	          <label for="password">填写密码</label>
            <span v-if="!(passwordTooShort || passwordTooLong) && password" class="success-icon fas fa-check-circle" style="color: green;
            position: absolute;
            margin-left: 35%;"></span>
            <span v-if="(passwordTooShort || passwordTooLong) && password" class="error-password" style="margin-left:19% ;">密码长度请在{{ passwordMinLength }}~{{ passwordMaxLength }}之间</span>
          </div>
	          <input type="password" v-model="password" name="password" placeholder="请输入密码" @input="validatePasswordLength" />
        </div>
	      <div class="form-group">
          <div class="password-input">
            <label for="password">确认密码</label>
            <span v-if="!passwordError && confirmPassword" class="success-icon fas fa-check-circle" style="color: green;
            position: absolute;
            margin-left: 35%;"></span>
            <span v-if="passwordError && confirmPassword" class="error-password">前后密码不一致</span>
          </div>
          <input type="password" v-model="confirmPassword" name="confirmPassword" placeholder="请确认密码" @input="validconfirmPassword"/>
        </div>
	      <button type="submit" @click="submitForm">注册</button>
	    </form>
	    <div class="register-link">
	      已有账号？<router-link to="/login">去登录</router-link>
	    </div>
	  </div>
	</div>
</template>

<style scoped>
html, body {
  height: 100%; /* 设置网页高度为100% */
  margin: 0; /* 清除默认的页面边距 */
  overflow: hidden; /* 禁止页面滚动 */
}

.form-container h1 {
  text-align: center;
  color: #fff;
}

.form-group {
  margin-bottom: 20px; /* 设置表单项目之间的垂直间距 */
  margin-left:10%;
}

.form-container {
  position: absolute;
  width: 60%;
  height: 60%;
  top: 20%;
  right: 5%;
  display: flex;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
  background-color: #7A7A7A;
  background-color: rgba(122, 122, 122, 0.5);
  padding: 8px 12px;
}

.left-half {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.right-half {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.right-half form {
  display: flex;
  flex-direction: column;
  margin-top: 12%;
}

.province-select {
  /* 调整下拉选项的宽度 */
  width: 40%;
  /* 调整下拉选项的高度 */
  height: 50%;
  /* 调整下拉选项的字体大小 */
  font-size: 16px;
  /* 调整下拉选项的背景颜色 */
  background-color: #ffffff;
  /* 调整下拉选项的边框样式 */
  border: 1px solid #cccccc;
  /* 调整下拉选项的边框圆角 */
  border-radius: 4px;
  /* 调整下拉选项的内边距 */
  padding: 6px;
  /* 调整下拉选项的颜色 */
  color: #333333;
}
   
.city-select {
  /* 调整下拉选项的宽度 */
  width: 40%;
  /* 调整下拉选项的高度 */
  height: 50%;
  /* 调整下拉选项的字体大小 */
  font-size: 16px;
  /* 调整下拉选项的背景颜色 */
  background-color: #ffffff;
  /* 调整下拉选项的边框样式 */
  border: 1px solid #cccccc;
  /* 调整下拉选项的边框圆角 */
  border-radius: 4px;
  /* 调整下拉选项的内边距 */
  padding: 6px;
  /* 调整下拉选项的颜色 */
  color: #333333;
  margin-left: 5%;
}
   
.phone-input {
  display: flex;
  align-items: center;
}
.password-input {
  display: flex;
  align-items: center;
}


.phone-input .success-icon {
  display: inline-block; 
}

.error-phone {
  color: red;
  position: absolute;
  margin-left: 22%;
}
.error-password {
  color: red;
  position: absolute;
  margin-left: 25%;
}

.register-link {
  color: #fff; /* 设置链接的文字颜色 */
  text-decoration: none; /* 移除链接的下划线 */
  margin-left:10%;
  margin-top: 20px;
}
  
.register-link a {
  color: #007bff; /* 设置链接的文字颜色 */
}

label {
  display: block; /* 让标签元素独占一行 */
  margin-bottom: 5px; /* 调整标签和输入框之间的垂直间距 */
  color:#fff;
}

input[type="text"],
input[type="password"]
{
  width: 80%; /* 设置输入框宽度为100% */
  padding: 10px; /* 调整输入框的内边距 */
  border: 1px solid #ccc; /* 设置输入框的边框样式 */
  border-radius: 4px; /* 设置输入框的圆角 */
}


button[type="submit"] {
  width: 77%;
  padding: 10px 20px; /* 调整按钮的内边距 */
  background-color: #007bff; /* 设置按钮的背景颜色 */
  color: #fff; /* 设置按钮的文字颜色 */
  border: none; /* 移除按钮的边框 */
  border-radius: 4px; /* 设置按钮的圆角 */
  cursor: pointer; /* 设置按钮的鼠标样式为手型 */
  margin-left: 10%;
}


.register-link a:hover {
  text-decoration: underline; /* 鼠标悬停时添加下划线效果 */
}


</style>

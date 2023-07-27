<script setup>
import { ref } from 'vue';
import axios from "axios";

const username= ref('');
const password= ref('');

const submitForm = (event) => {
    // event.preventDefault();
    const data = {
        username: username.value,
        password: password.value
    };
    axios
        .post('/api/login', data)
        .then(response => {
          // 处理响应
          alert(`Hello ${state.username}!，登录成功！`);
        })
        .catch(error => {
          alert('登录失败！');
          console.error('请求出错:', error);
        });
};

</script>


<template>
	<div class="form-container">
	        <!-- 登录表单 -->
		<form>
			<h1>Welcome!</h1>
		    <div class="form-group">
		      <label for="username">用户名</label>
		      <input type="text" v-model="username" name="username" placeholder="请输入账号" />
		    </div>
		
		    <div class="form-group">
		      <label for="password">密码</label>
		      <input type="password" v-model="password" name="password" placeholder="请输入密码" />
		    </div>
		
		    <div class="form-group">
		      <label class="checkbox-label" for="remember">
		        <input type="checkbox" id="remember" name="remember" />
		        记住密码
		      </label>
		    </div>
		
		    <button type="submit" @click="submitForm">登录</button>
		  </form>
		
		  <div class="register-link">
		    没有账号？<router-link to="/register">这里注册</router-link>
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
	width: 30%;
	height: 60%;
	top: 20%; 
	right: 5%; 
	display: flex;
	/* align-items: center; */
	box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2); /* 添加黑色阴影底板 */
	background-color: #7A7A7A; 
	background-color: rgba(122, 122, 122, 0.5); /* 设置底板为半透明的 #7A7A7A */
	padding: 8px 12px; /* 调整底板内边距 */
	flex-direction: column;
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

.checkbox-label {
  display: flex;
  align-items: center;
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

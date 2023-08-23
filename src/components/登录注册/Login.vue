<script setup>
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { useUserStore } from '@/store/user'
import background from './login-register.vue'

const router = useRouter()
const userStore = useUserStore()

const form = ref({
  username:'',
  password:''
})

const rules = {
  username: [
    { required:true,message:'手机号不能为空',trigger:'blur'}
  ],
  password:[
    { required:true,message:'密码不能为空',trigger:'blur'},
    { min:6, max:14,message:'密码长度在6~14之间',trigger:'blur'}
  ]
}

const validateForm = () => {
  usernameError.value = username.value.trim() === '';
  passwordError.value = password.value.trim() === '';
};

const formRef = ref(null)

const submitForm = async () => {
  formRef.value.validate(async (valid) => {
    const { userphone, password } = form.value;
    if (valid) {
        await userStore.getUserInfo({ userphone, password });
          ElMessage({ type: 'success', message: '登录成功' });
          if (userStore.userInfo.Role === 'User') {
            router.replace({ path: '/' });
          } else {
            router.replace({ path: '/manager' });
          }
    }
  });
};


</script>


<template>
  <background></background>
	<div class="form-container">
	  <!-- 登录表单 -->
		<form>
			<h1>欢迎登录</h1>
      <h2>请输入您的手机号和密码</h2>
      <el-form ref="formRef" :model="form" :rules="rules" label-position="center" label-width="60px" status-ico>
        <div class="inputtext">
          <img src="  ../../../public/224用户.png" style="height:20px;width: 20px;margin-right: 0;">
          <label for="phone">手机号</label>
        </div>
       
        <el-form-item prop="username"><el-input class="custom-input" resize="true" v-model="form.username"/></el-form-item>
        <div class="inputtext">
          <img src="  ../../../public/pswd.png" style="height:20px;width: 20px;">
          <label for="phone">密码</label>
        </div>
        <el-form-item prop="password"><el-input class="custom-input" resize="true" type="password" v-model="form.password"/></el-form-item>
      </el-form>
		  <el-button type="button" style="background-color:#729cd4" @click="submitForm">点击登录</el-button>
		</form>
    <br>
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

.form-container {
  text-align: left;
}

.form-container h1 {
  text-align: center;
  color: #4e73a3;
}

.form-container h2 {
  text-align: center;
  color: #8fa6c5;
  font-size: 14px;
  font-weight: lighter;
  margin-top: 2px;
}

.custom-input {
  margin-top: 10px;
  margin-bottom: 15px;
  height:60%;
  width: 85%;
}

.form-container {
	position: absolute;
	width: 30%;
	height: 60%;
	top: 20%; 
	right: 5%; 
	display: flex;
	/* box-shadow: 0 2px 4px rgba(141, 141, 141, 0.2);  */
	background-color: #ffffff;  
	padding: 8px 12px; 
	flex-direction: column;
  border-radius: 10px;
  }
  
.register-link {
  color: #496180; /* 设置链接的文字颜色 */
  text-decoration: none; /* 移除链接的下划线 */
  margin-left:10%;
  margin-top: 20px;
}
  
.register-link a {
  color: #5f84b4; /* 设置链接的文字颜色 */
}


label {
  display: block; /* 让标签元素独占一行 */
  margin-left:3%;
  color:#729cd4;
  font-size: 15px;
}


button[type="button"] {
  width: 74%;
  padding: 10px 20px; /* 调整按钮的内边距 */
  background-color: #729cd4; /* 设置按钮的背景颜色 */
  color: #fff; /* 设置按钮的文字颜色 */
  border: none; /* 移除按钮的边框 */
  border-radius: 4px; /* 设置按钮的圆角 */
  cursor: pointer; /* 设置按钮的鼠标样式为手型 */
  margin-left: 13%;
  margin-top: 20px;
}

.register-link {
  margin-left: 13%;
}

.register-link a:hover {
  text-decoration: underline; /* 鼠标悬停时添加下划线效果 */
}

.inputtext{
  display:flex;
  margin-left: 12%;
}

</style>

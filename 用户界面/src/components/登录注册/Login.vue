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
    { required:true,message:'账号不能为空',trigger:'blur'}
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

const submitForm = () => {
    formRef.value.validate(async(valid)=>{
      const {username,password} = form.value
      if(valid){
        await userStore.getUserInfo({username,password})
        ElMessage({type:'success',message:'登录成功'})
        router.replace({path:'/'})
      }
    })
};

</script>


<template>
  <background></background>
	<div class="form-container">
	  <!-- 登录表单 -->
		<form>
			<h1>账户登录</h1>
      <el-form ref="formRef" :model="form" :rules="rules" label-position="center" label-width="60px" status-ico>
        <label for="phone">账号</label>
        <el-form-item prop="username"><el-input class="custom-input" resize="true" v-model="form.username"/></el-form-item>
        <label for="phone">密码</label>
        <el-form-item prop="password"><el-input class="custom-input" resize="true" type="password" v-model="form.password"/></el-form-item>
      </el-form>
		  <button type="button" @click="submitForm" style="margin-top: 20px;">点击登录</button>
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

.form-container {
  text-align: left;
}

.form-container h1 {
  text-align: center;
  color: #fff;
}

.custom-input {
  margin-top: 15px;
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
  margin-left: 13%;
  color:#fff;
}


button[type="button"] {
  width: 74%;
  padding: 10px 20px; /* 调整按钮的内边距 */
  background-color: #007bff; /* 设置按钮的背景颜色 */
  color: #fff; /* 设置按钮的文字颜色 */
  border: none; /* 移除按钮的边框 */
  border-radius: 4px; /* 设置按钮的圆角 */
  cursor: pointer; /* 设置按钮的鼠标样式为手型 */
  margin-left: 13%;
}

.register-link {
  margin-left: 13%;
}

.register-link a:hover {
  text-decoration: underline; /* 鼠标悬停时添加下划线效果 */
}

</style>

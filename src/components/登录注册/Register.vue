<script setup>
import { ref, watch} from 'vue';
import { registerAPI } from "@/api/user"
import { ElMessage } from 'element-plus'
import { useRouter } from 'vue-router'
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
const passwordError0 = ref(false);
const passwordTooShort = ref(false);
const passwordTooLong = ref(false);
const passwordMinLength = 8;
const passwordMaxLength = 14;

const router = useRouter();

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

const handlePhoneInput = () => {
  console.log(phone.value)
  // 获取输入框的值并移除所有非数字字符
  const digitsOnly = phone.value.replace(/\D/g, "");

  // 在第4个和第9个位置插入空格
  const formattedValue = insertSpaces(digitsOnly, [3, 7]);

  phone.value = formattedValue;
};

const insertSpaces = (str, positions) => {
  const result = [];
  let positionIndex = 0;

  for (let i = 0; i < str.length; i++) {
    if (positionIndex < positions.length && i === positions[positionIndex]) {
      result.push(" ");
      positionIndex++;
    }
    result.push(str[i]);
  }

  return result.join("");
};

const validatePhoneNumber = () => {
  const phoneNumberPattern = /^1(3[0-9]|4[57]|5[0-35-9]|7[0135678]|8[0-9]|98|99)\s\d{4}\s\d{4}$/;
  phoneError.value = !phoneNumberPattern.test(phone.value);
};

const validconfirmPassword = () => {
  const passwordRegex = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()]).{8,14}$/;
  
  if (password.value !== confirmPassword.value) {
    passwordError.value = true;
  }  else {
    passwordError.value = false;
  }
  if (!passwordRegex.test(password.value)) {
    passwordError0.value = true;
  }
}

const submitForm = async() => {

    // 先进行错误检测
    validatePhoneNumber();
    validatePasswordLength();
    validconfirmPassword();

    // 检查是否有错误
    if (phoneError.value) {
      ElMessage({ type: 'warning', message: '电话号码填入有误，请修改' });
      return;
    }
    if (passwordTooShort.value) {
      ElMessage({ type: 'warning', message: '密码过短，请修改' });
      return;
    }
    if (passwordTooLong.value) {
      ElMessage({ type: 'warning', message: '密码过长，请修改' });
      return;
    }
    if (passwordError.value) {
      ElMessage({ type: 'warning', message: '前后密码不一致' });
      return;
    }
    if (passwordError0.value) {
      ElMessage({ type: 'warning', message: '密码格式有误，密码长度在8~14之间，必须包含数字、大小写字母、特殊字符（!@#$%^&*()）' });
      return;
    }
    const data = {
      username: username.value,
      password: password.value,
      phoneNumber: phone.value,
      city: selectedProvince.value+selectedCity.value,
    };
    console.log(data)
    try {
        const res = await registerAPI(data);
        ElMessage({type:'success',message:res})
        // userStore.userInfo.
        router.push('/')
        console.log(res); // 输出注册API返回的结果到控制台
    } catch (error) {
        console.error('出错'); // 如果有错误发生，输出错误到控制台
    }
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
          <input type="text" v-model="phone" name="phone" placeholder="请输入电话" @input="handlePhoneInput"/>
      </div>
      <div class="form-group">
        <label for="region">选择地区</label>
        <select v-model="selectedProvince" class="province-select">
          <option v-for="(key,value) in provinces" :value="key" :key="key">{{ value }}</option>
        </select>
        <select v-model="selectedCity" name="cities" class="city-select">
            <option v-for="city in cities" :value="city.value" :key="city.key">{{ city.key }}</option>
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
          </div>
          <div class="custom-input-container">
            <el-tooltip 
            placement="right" 
            content="密码长度在8~14之间，必须包含数字、大小写字母、特殊字符（/!@#$%^&*()）" 
            open-delay="500"
            effect="light">
              <div class="input-with-tooltip">
                <input type="password" v-model="password" name="password" placeholder="请输入密码" @input="validatePasswordLength" />
              </div>
            </el-tooltip>
          </div>
           
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
        <button type="button" @click="submitForm">注册</button>
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
  color: #4e73a3;
  font-size:26px;
}

.form-group {
  margin-bottom: 20px; /* 设置表单项目之间的垂直间距 */
  margin-left:10%;
}

.form-container {
  position: absolute;
  /* width: 60%;
  height: 60%;
  top: 20%;
  right: 5%;
  display: flex;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
  background-color: #7A7A7A;
  background-color: rgba(122, 122, 122, 0.5);
  padding: 8px 12px; */
  width:40vw;
  height:90vh;
  right:0%;
  top:5%;
  align-items: center;
  justify-content: center;
  align-items: center;
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
  margin-top: 1%;
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
  margin-left: 15%;
}

.register-link {
  color: #4e73a3; /* 设置链接的文字颜色 */
  text-decoration: none; /* 移除链接的下划线 */
  margin-left:10%;
  margin-top: 20px;
}
  
.register-link a {
  color: #729cd4; /* 设置链接的文字颜色 */
}

label {
  display: block; /* 让标签元素独占一行 */
  margin-bottom: 3px; /* 调整标签和输入框之间的垂直间距 */
  color:#4e73a3;
  font-size:14px;

}

input[type="text"],
input[type="password"]
{
  width: 80%; /* 设置输入框宽度为100% */
  padding: 10px; /* 调整输入框的内边距 */
  border: 1px solid #ccc; /* 设置输入框的边框样式 */
  border-radius: 4px; /* 设置输入框的圆角 */
}


button[type="button"] {
  width: 76%;
  padding: 10px 20px; /* 调整按钮的内边距 */
  background-color: #729cd4; /* 设置按钮的背景颜色 */
  color: #fff; /* 设置按钮的文字颜色 */
  border: none; /* 移除按钮的边框 */
  border-radius: 4px; /* 设置按钮的圆角 */
  cursor: pointer; /* 设置按钮的鼠标样式为手型 */
  margin-left: 10%;
  margin-top: 20px;
}

.register-link a:hover {
  text-decoration: underline; /* 鼠标悬停时添加下划线效果 */
}

.password-requirement {
      font-family: Arial, sans-serif;
      font-size: 12px;
      color: #fff;
      border: 1px solid #ddd;
      padding: 10px;
      border-radius: 5px;
      max-width: 300px;
      margin: 0 auto;
      margin-top: 60px;
    }

.password-requirement::before {
  content: '*';
  color: red;
  margin-right: 5px;
}


</style>

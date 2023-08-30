<template>
  <div class="tableitem" >
        <img src="@/photos/狗狗头像.jpeg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
        <el-text class="welcome-text" size="Large">寄养申请表</el-text>
  </div>
  <el-form :model="form" label-width="120px">
    <el-form-item label="宠物昵称">
      <el-input v-model="form.name"  style="width: 50%"/>
    </el-form-item>
    <el-form-item label="宠物类型">
    <el-row>
      <el-radio-group v-model="radio">
    <el-radio  label="猫" value="猫">猫猫</el-radio>
    <el-radio label="狗" value="狗">狗狗</el-radio>
  </el-radio-group>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;
  <el-radio-group v-model="size_radio" size="small" v-if="radio==='狗'">
      <el-radio-button label="大型犬" />
      <el-radio-button label="中型犬" />
      <el-radio-button label="小型犬" />
    </el-radio-group>
    </el-row>


    </el-form-item>
    <el-form-item label="寄养起始时间">
      <el-col :span="11">
        <el-date-picker
          v-model="form.date"
          type="date"
          placeholder="Pick a date"
          style="width: 100%"
        />
      </el-col>
      <el-col :span="2" class="text-center">
        <span class="text-gray-500" style="margin-left: 30px;">-</span>
      </el-col>
      <el-col :span="11">
       
      </el-col>

    </el-form-item>
    <el-form-item label="寄养天数">
      <el-input-number v-model="form.num" :min="1" :max="1000"  />
    </el-form-item>
    <el-form-item label="寄养费用">
        <el-text class="mx-1">{{ calculatedFee }}元</el-text>
      </el-form-item>
    <el-form-item label="备注">
      <el-input v-model="form.remark" type="textarea" />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmit">提交</el-button>
    </el-form-item>

  </el-form>
</template>

<script lang="ts" setup>
import { reactive,computed,ref,watch } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import axios from 'axios';
import { useUserStore } from '@/store/user';
import { submitFosterApplication } from '@/api/pet_foster'; 
import { useRouter } from 'vue-router'

const router = useRouter()
const userStore = useUserStore();

// do not use same name with ref
const form = reactive({
  name: '',
  date: '',
  num:'',
  remark: ''
})

const radio = ref("猫")
const size_radio = ref('大型犬')

const onSubmit = () => {
  ElMessageBox.confirm(
    '点击确认将提交申请，要继续吗？',
    '确认',
    {
      confirmButtonText: 'OK',
      cancelButtonText: 'Cancel',
      type: 'warning',
    }
  )
    .then(() => {
      // 构造数据对象，将表单中的值传递给它
      // const foster_table = {
      //   user:userStore.userInfo.User_ID,
      //   name: form.name,
      //   type: radio.value, 
      //   size: size_radio.value,
      //   date: form.date,
      //   num: form.num,
      //   remark: form.remark,
      // };

      // axios.post('/api/pet_foster', foster_table)
      submitFosterApplication(userStore.userInfo, form, radio.value, size_radio.value)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
              router.push('/');
            })
            .catch(error => {
              // 处理错误
              ElMessage({
                type: 'error',
                message: '提交失败',
              });
            });
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: '取消提交',
      })
    })
}

const calculatedFee = computed(function() {
  const duration = Number(form.num) || 0;
  let fee = 0;
  
  if (size_radio.value === '大型犬'&&radio.value!="猫") {
    fee = 30 * duration;
  } else if (size_radio.value === '中型犬') {
    fee = 25 * duration;
  } else{
    fee = 20 * duration;
  }

  return fee;
});
  const value = ref([])

</script>

<style>
.tableitem {
  display: flex;
  /* justify-content: center; */
}
.tableitem > * {
  margin-right: 10px;
  flex: 0 0 auto;
}

</style>

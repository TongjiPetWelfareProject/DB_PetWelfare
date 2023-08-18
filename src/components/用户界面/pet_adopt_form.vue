<template>
  <div class="tableitem" >
    <img src="./photos/柯基头像.jpg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
    <p class="welcome-text">宠物领养申请表</p>
  </div>
      <el-form :model="form" label-width="200px">
    <el-form-item label="领养人ID">
      <el-input v-model="UID" readonly style="width: 600px;" />
    </el-form-item>
    <el-form-item label="领养人用户名">
      <el-input v-model="U_name" readonly style="width: 600px;" />
    </el-form-item>
    <el-form-item label="宠物ID">
      <el-input v-model="PID" readonly style="width: 600px;" />
    </el-form-item>
    <el-form-item label="宠物名字">
      <el-input v-model="P_name" readonly style="width: 600px;" />
    </el-form-item>
    <el-form-item label="领养人性别">
      <el-radio-group v-model="form.gender">
        <el-radio label="男" :value="true"/>
        <el-radio label="女" :value="false"/>
      </el-radio-group>
    </el-form-item>
    <el-form-item label="有几年饲养经验">
      <el-col :span="11">
        <el-input-number v-model="form.pet_exp" :min="0" :max="100" />
      </el-col>
    </el-form-item>
    <el-form-item label="是否可以坚持长期养护">
      <el-radio-group v-model="form.long_term_care">
        <el-radio label="是" :value="true"/>
        <el-radio label="否" :value="false"/>
      </el-radio-group>
    </el-form-item>
    <el-form-item label="宠物生病是否愿意为它治病">
      <el-radio-group v-model="form.w_to_treat">
        <el-radio label="是" :value="true"/>
        <el-radio label="否" :value="false"/>
      </el-radio-group>
    </el-form-item>
    <el-form-item label="每天能留出多少时间照顾猫狗">
      <el-col :span="11">
        <el-input-number v-model="form.d_care_h" :min="0" :max="24" />
      </el-col>
    </el-form-item>
    <el-form-item label="领养后主要照顾猫狗的人是谁">
      <el-input v-model="form.P_caregiver" style="width: 600px;"/>
    </el-form-item>
    <el-form-item label="家庭居住人口">
      <el-col :span="11">
        <el-input-number v-model="form.f_popul" :min="0" :max="100" />
      </el-col>
    </el-form-item>
    <el-form-item label="家庭中是否有孩子">
      <el-radio-group v-model="form.be_children">
        <el-radio label="是" :value="true"/>
        <el-radio label="否" :value="false"/>
      </el-radio-group>
    </el-form-item>
    <el-form-item label="是否愿意接受志愿者微信（视频、照片）的上门回访">
      <el-radio-group v-model="form.accept_vis">
        <el-radio label="是" :value="true"/>
        <el-radio label="否" :value="false"/>
      </el-radio-group>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmit">提交</el-button>
      <el-button>取消</el-button>
    </el-form-item>
  </el-form>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import axios from 'axios';
import { useUserStore } from '@/store/user';
import { submitAdoptApplication } from '@/api/pet_adopt'; 

const userStore = useUserStore(); // 请确保导入 useUserStore

// do not use same name with ref
const form = reactive({
  gender: true,
  pet_exp: 0,
  long_term_care: true,
  w_to_treat: true,
  d_care_h: 0,
  P_caregiver: '',
  f_popul: 0,
  be_children: true,
  accept_vis: true,
})

const UID = '12345';
const U_name = '柴犬の奴';
const PID = '67890';
const P_name = '花花';

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
        // axios.post('/api/pet_foster', foster_table)
        submitAdoptApplication(userStore.userInfo, form)
              .then(response => {
                // 处理后端返回的响应
                ElMessage({
                  type: 'success',
                  message: '提交成功',
                });
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
</script>

<style scoped>
.tableitem {
  display: flex;
  align-items: center;
}

.tableitem img {
  flex: 0 0 auto;
}

.welcome-text {
  flex: 1;
}


</style>
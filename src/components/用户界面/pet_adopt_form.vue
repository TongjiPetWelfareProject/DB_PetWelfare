<template>
  <div class="tableitem" >
    <img src="@/photos/柯基头像.jpg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
    <p class="welcome-text">宠物领养申请表</p>
  </div>
  <el-form :model="form" label-width="200px">
    <el-form-item label="领养人姓名">
      <el-input v-model="form.name" />
    </el-form-item>
    <el-form-item label="领养人性别">
      <el-radio-group v-model="form.gender">
        <el-radio label="男" />
        <el-radio label="女" />
      </el-radio-group>
    </el-form-item>
    <el-form-item label="领养人手机号">
      <el-input v-model="form.phone" />
    </el-form-item>
    <el-form-item label="领养时间">
      <el-col :span="11">
        <el-date-picker
          v-model="form.date"
          type="date"
          placeholder="选择日期"
          style="width: 100%"
        />
      </el-col>
    </el-form-item>
    <el-form-item label="有无猫狗饲养经验">
      <el-radio-group v-model="form.choice1">
        <el-radio label="有" />
        <el-radio label="无" />
      </el-radio-group>
    </el-form-item>
    <el-form-item label="有几年饲养经验" v-if="form.choice1 === '有'">
      <el-col :span="11">
        <el-input-number v-model="form.choice2" :min="0" :max="100" />
      </el-col>
    </el-form-item>
    <el-form-item label="是否可以坚持长期养护">
      <el-radio-group v-model="form.choice3">
        <el-radio label="是" />
        <el-radio label="否" />
      </el-radio-group>
    </el-form-item>
    <el-form-item label="宠物生病是否愿意为它治病">
      <el-radio-group v-model="form.choice4">
        <el-radio label="是" />
        <el-radio label="否" />
      </el-radio-group>
    </el-form-item>
    <el-form-item label="每天能留出多少时间照顾猫狗">
      <el-col :span="11">
        <el-input-number v-model="form.choice5" :min="0" :max="24" />
      </el-col>
    </el-form-item>
    <el-form-item label="领养后主要照顾猫狗的人是谁">
      <el-input v-model="form.choice6" type="textarea" />
    </el-form-item>
    <el-form-item label="家庭居住人口">
      <el-col :span="11">
        <el-input-number v-model="form.choice7" :min="0" :max="100" />
      </el-col>
    </el-form-item>
    <el-form-item label="家庭中是否有孩子">
      <el-radio-group v-model="form.choice8">
        <el-radio label="是" />
        <el-radio label="否" />
      </el-radio-group>
    </el-form-item>
    <el-form-item label="是否愿意接受志愿者微信（视频、照片）的上门回访">
      <el-radio-group v-model="form.choice9">
        <el-radio label="是" />
        <el-radio label="否" />
      </el-radio-group>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmit">提交</el-button>
      <el-button>取消</el-button>
    </el-form-item>
  </el-form>
</template>

<script lang="ts" setup>
import { reactive } from 'vue'

// do not use same name with ref
const form = reactive({
  name: '',
  gender: '',
  phone: '',
  date: '',
  choice1: '',
  choice2: 0,
  choice3: '',
  choice4: '',
  choice5: 0,
  choice6: '',
  choice7: 0,
  choice8: '',
  choice9: '',
})

const onSubmit = () => {
  console.log('submit!')
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
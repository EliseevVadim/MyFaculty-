import Vue from 'vue'
import Vuex from 'vuex'
import scienceRanks from "@/store/modules/scienceRanks";
import secondaryObjectTypes from "@/store/modules/secondaryObjectTypes";
import pairInfos from "@/store/modules/pairInfos";
import courses from "@/store/modules/courses";
import pairRepeatings from "@/store/modules/pairRepeatings";
import daysOfWeek from "@/store/modules/daysOfWeek";
import groups from "@/store/modules/groups";
import disciplines from "@/store/modules/disciplines";
import floors from "@/store/modules/floors";
import secondaryObjects from "@/store/modules/secondaryObjects";
import teachers from "@/store/modules/teachers";
import auditoriums from "@/store/modules/auditoriums";
import teachersDisciplines from "@/store/modules/teachersDisciplines";
import pairs from "@/store/modules/pairs";

Vue.use(Vuex)

export default new Vuex.Store({
  state: {},
  getters: {},
  mutations: {},
  actions: {},
  modules: {
      scienceRanks,
      secondaryObjectTypes,
      pairInfos,
      courses,
      pairRepeatings,
      daysOfWeek,
      groups,
      disciplines,
      floors,
      secondaryObjects,
      teachers,
      auditoriums,
      teachersDisciplines,
      pairs
  }
})

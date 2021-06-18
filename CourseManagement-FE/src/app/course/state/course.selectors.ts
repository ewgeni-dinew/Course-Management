import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CourseState } from "./course.reducer";

const getCourseFeatureState = createFeatureSelector<CourseState>('courses'); //main selector for the 'courses' feature

export const getSelectedCourse = createSelector(getCourseFeatureState, state => state.selectedCourse);

export const getSelectedFavCourse = createSelector(getCourseFeatureState, state => state.selectedFavCourse);
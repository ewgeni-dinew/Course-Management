import { createFeatureSelector, createSelector } from "@ngrx/store";
import { State } from "./course.reducer";

const getCourseFeatureState = createFeatureSelector<State>('courses'); //main selector for the 'courses' feature

export const getSelectedCourse = createSelector(getCourseFeatureState, state => state.courseState.object);

export const getCourseRating = createSelector(getCourseFeatureState, state => state.courseState.courseRating);

export const getSelectedCourseId = createSelector(getCourseFeatureState, state => state.courseState.courseId);

// for favorite course >>>
export const getSelectedFavCourse = createSelector(getCourseFeatureState, state => state.favCourseState.object);

export const getFavCourseRating = createSelector(getCourseFeatureState, state => state.favCourseState.courseRating);

export const getSelectedFavCourseId = createSelector(getCourseFeatureState, state => state.favCourseState.courseId);
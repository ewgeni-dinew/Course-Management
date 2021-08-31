import { createReducer, on } from "@ngrx/store";
import { ICourse } from "src/app/shared/contracts/course";
import * as AppState from "../../state/app.state";
import { deselectCourse, selectCourse, selectCourseRating, selectFavCourse } from "./course.actions";

export interface State extends AppState.State {
    courseState: CourseState,
    favCourseState: CourseState
}

export interface CourseState {
    object: ICourse,
    courseRating: number | null,
    courseId: number | null
}

const initialState: State = {
    courseState: {
        object: null,
        courseRating: null,
        courseId: null,
    },
    favCourseState: {
        object: null,
        courseRating: null,
        courseId: null,
    }
};

export const courseReducer = createReducer<State>(
    initialState,
    on(selectCourse, (state, { course }) => {

        let c: CourseState = {
            object: course,
            courseRating: course.rating,
            courseId: course.id
        };

        return {
            ...state,
            courseState: c
        }
    }),
    on(selectFavCourse, (state, { course }) => {

        let c: CourseState = {
            object: course,
            courseRating: course.rating,
            courseId: course.id
        };

        return {
            ...state,
            favCourseState: c
        }
    }),
    on(selectCourseRating, (state, { rating, isFavCourse }) => {

        let c: CourseState = {
            object: null, //is set before the return
            courseRating: rating,
            courseId: null //is set before the return
        };

        if (isFavCourse) {
            c.object = state.favCourseState.object;
            c.courseId = state.favCourseState.courseId;
            return {
                ...state,
                favCourseState: c
            }
        } else {
            c.object = state.courseState.object;
            c.courseId = state.courseState.courseId;
            return {
                ...state,
                courseState: c
            }
        }
    }),
    on(deselectCourse, (state, { isFavCourse }) => {

        let c: CourseState = {
            object: null,
            courseRating: null,
            courseId: null //is set before the return
        };

        if (isFavCourse) {
            c.courseId = state.favCourseState.courseId;
            return {
                ...state,
                favCourseState: c
            }
        } else {
            c.courseId = state.courseState.courseId;
            return {
                ...state,
                courseState: c
            }
        }
    }),
);
import { createReducer, on } from "@ngrx/store";
import { ICourse } from "src/app/shared/contracts/course";
import * as AppState from "../../state/app.state";
import { selectCourse, selectCourseRating, selectCourseShowDetails, selectFavCourse } from "./course.actions";

export interface State extends AppState.State {
    courses: CourseState
}

export interface CourseState {
    selectedCourse: ICourse,
    selectedFavCourse: ICourse,
    courseRating: number,
    showCourseDetails: boolean,
    showFavCourse: boolean
}

const initialState: CourseState = {
    selectedCourse: null,
    selectedFavCourse: null,
    courseRating: 0,
    showCourseDetails: false,
    showFavCourse: false,
};

export const courseReducer = createReducer<CourseState>(
    initialState,
    on(selectCourse, (state, { course }) => {
        return {
            ...state,
            selectedCourse: course
        }
    }),
    on(selectFavCourse, (state, { course }) => {
        return {
            ...state,
            selectedFavCourse: course
        }
    }),
    on(selectCourseRating, (state, { rating }) => {
        return {
            ...state,
            courseRating: rating
        }
    }),
    on(selectCourseShowDetails, (state, { flag }) => {
        return {
            ...state,
            showCourseDetails: flag
        }
    }),
);
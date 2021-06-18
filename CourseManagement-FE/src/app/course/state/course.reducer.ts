import { createReducer, on } from "@ngrx/store";
import { ICourse } from "src/app/shared/contracts/course";
import * as AppState from "../../state/app.state";
import { selectCourse, selectFavCourse } from "./course.actions";

export interface State extends AppState.State {
    courses: CourseState
}

export interface CourseState {
    selectedCourse: ICourse,
    selectedFavCourse: ICourse
}

const initialState: CourseState = {
    selectedCourse: null,
    selectedFavCourse: null
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
    })
);
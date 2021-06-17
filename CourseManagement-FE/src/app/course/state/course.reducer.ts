import { createAction, createReducer, on } from "@ngrx/store";
import { ICourse } from "src/app/shared/contracts/course";
import * as AppState from "../../state/app.state";

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
    on(createAction('[Course] Select course'), (state): CourseState => {
        return {
            ...state,
            selectedCourse: null
        };
    })
);
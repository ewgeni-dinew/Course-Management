import { createAction, props } from "@ngrx/store";
import { ICourse } from "src/app/shared/contracts/course";

export const selectCourse = createAction(
    '[Course] Select course',
    props<{ course: ICourse }>()
);

export const selectFavCourse = createAction(
    '[Course] Select favorite course',
    props<{ course: ICourse }>()
);

export const selectCourseRating = createAction(
    '[Course] Select course rating',
    props<{ rating: number }>()
);

export const selectCourseShowDetails = createAction(
    '[Course] Show course details',
    props<{ flag: boolean }>()
);
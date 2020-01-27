import { DirectlyRelatedToUser } from './_basemodels/directlyrelatedtouser';

export interface Photo extends DirectlyRelatedToUser {
    url: string;
    dateAdded: Date;
    isMain: boolean;
}

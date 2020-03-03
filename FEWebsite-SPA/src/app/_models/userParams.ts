export interface UserParams {
    minAge: number;
    maxAge: number;
    genderId: string;
    orderBy: string;
    likeFilter: string;
}

export const UserParamsOptions = {
    OrderBy: {
        lastLogin: 'lastLogin',
        accountCreated: 'accountCreated',
    },
    LikeFilter: {
        all: 'all',
        likees: 'likees',
        likers: 'likers',
    }
};

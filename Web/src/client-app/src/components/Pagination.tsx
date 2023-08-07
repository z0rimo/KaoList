import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../ClassNameHelper";
import LazyCircleChevronRighticon from "../svgs/LazyCircleChevronRighticon";
import PageLink from "./PageLink";

interface IPagintaionProps {
    totalPosts?: number;
    postsPerPage?: number;
}

function Pagination({ totalPosts, postsPerPage }: IPagintaionProps) {
    const location = useLocation();
    const navigate = useNavigate();
    const currentParams = new URLSearchParams(location.pathname);
    const currentPage = Number(currentParams.get("page")) || 1;
    const [page, setPage] = useState(currentPage);
    const [currentGroup, setCurrentGroup] = useState(1);

    const numOfPages: number[] = [];
    for (let i = 1; i <= Math.ceil(totalPosts! / postsPerPage!); i++) {
        numOfPages.push(i);
    }

    const handlePageClick = React.useCallback((num: number) => {
        if (num === undefined) {
            return;
        }
        navigate(`${location.pathname}?page=${num}`);
        setPage(num);
    }, [setPage, navigate, location.pathname]);

    const handlePrevBtn = () => {
        const newPage = page - 10 > 0 ? page - 10 : 1;
        setPage(newPage);
        navigate(`${location.pathname}?page=${newPage}`);
        if (currentGroup > 1) {
            setCurrentGroup(currentGroup - 1);
        }
    };

    const handleNextBtn = () => {
        const newPage = page + 10 <= numOfPages.length ? page + 10 : numOfPages.length;
        setPage(newPage);
        navigate(`${location.pathname}?page=${newPage}`);
        if (currentGroup < Math.ceil(numOfPages.length / 10)) {
            setCurrentGroup(currentGroup + 1);
        }
    };

    const groupStartIndex = (currentGroup - 1) * 10;
    const groupEndIndex = groupStartIndex + 10;

    return (
        <div className="pagination middle-layout">
            <LazyCircleChevronRighticon onClick={handlePrevBtn}
                className={ClassNameHelper.concat('left', currentGroup === 1 && 'disable')}
            />
            {numOfPages
                .slice(groupStartIndex, groupEndIndex)
                .map((num) => (
                    <PageLink
                        key={num}
                        no={num}
                        base={location.pathname}
                        className={page === num ? "active" : ''}
                        onClick={(num) => handlePageClick(num)}
                    />
                ))
            }
            <LazyCircleChevronRighticon onClick={handleNextBtn}
                className={ClassNameHelper.concat('', currentGroup === Math.ceil(numOfPages.length / 10) && 'disable')}
            />
        </div>
    );
}

export default React.memo(Pagination);
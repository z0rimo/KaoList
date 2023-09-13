import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../ClassNameHelper";
import LazyCircleChevronRighticon from "../svgs/LazyCircleChevronRighticon";
import PageLink from "./PageLink";
import { IPageInfo } from "../api/kaolistApi";

function Pagination({ totalResults = 0, resultsPerPage = 10 }: IPageInfo) {
    const location = useLocation();
    const navigate = useNavigate();
    const currentParams = new URLSearchParams(location.search);
    const initialCurrentPage = Number(currentParams.get("page")) || 1;
    const [page, setPage] = useState(initialCurrentPage);
    const [currentGroup, setCurrentGroup] = useState(1);

    const maxPageNumber = Math.ceil(totalResults / resultsPerPage);

    React.useEffect(() => {
        const currentPage = Number(currentParams.get("page")) || 1;
        setPage(currentPage);
    }, [location]);

    const handlePageClick = (num: number) => {
        const currentParams = new URLSearchParams(location.search);
        currentParams.set("page", String(num));
        navigate(`${location.pathname}?${currentParams.toString()}`);
        setPage(num);
    };
    
    const handlePrevBtn = () => {
        const newPage = Math.max(1, page - 10);
        const currentParams = new URLSearchParams(location.search);
        currentParams.set("page", String(newPage));
        navigate(`${location.pathname}?${currentParams.toString()}`);
        setPage(newPage);
        if (currentGroup > 1) {
            setCurrentGroup(currentGroup - 1);
        }
    };
    
    const handleNextBtn = () => {
        const newPage = Math.min(maxPageNumber, page + 10);
        const currentParams = new URLSearchParams(location.search);
        currentParams.set("page", String(newPage));
        navigate(`${location.pathname}?${currentParams.toString()}`);
        setPage(newPage);
        if (currentGroup < Math.ceil(maxPageNumber / 10)) {
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
            {Array.from({ length: maxPageNumber }, (_, i) => i + 1)
                .slice(groupStartIndex, groupEndIndex)
                .map((num) => (
                    <PageLink
                        key={num}
                        no={num}
                        base={location.pathname}
                        className={page === num ? "active" : ''}
                        onClick={() => handlePageClick(num)}
                    />
                ))
            }
            <LazyCircleChevronRighticon onClick={handleNextBtn}
                className={ClassNameHelper.concat('', currentGroup === Math.ceil(maxPageNumber / 10) && 'disable')}
            />
        </div>
    );
}

export default React.memo(Pagination);
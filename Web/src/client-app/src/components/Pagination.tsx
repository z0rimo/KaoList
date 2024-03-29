import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../ClassNameHelper";
import PageLink from "./PageLink";
import { IPageInfo } from "../api/kaolistApi";
import LazyCaretRightSolidIcon from "../svgs/LazyCaretRightSolidIcon";
import LazyCaretLeftSolidIcon from "../svgs/LazyCaretLeftSolidIcon";

interface IPaginationProps extends IPageInfo {
    className?: string;
}

function Pagination({ totalResults = 0, resultsPerPage = 10, className }: IPaginationProps) {
    const location = useLocation();
    const navigate = useNavigate();
    const currentParams = new URLSearchParams(location.search);
    const query = currentParams.get('q');
    const initialCurrentPage = Number(currentParams.get("page")) || 1;
    const [page, setPage] = useState(initialCurrentPage);
    const [currentGroup, setCurrentGroup] = useState(1);

    const maxPageNumber = Math.ceil(totalResults / resultsPerPage);

    React.useEffect(() => {
        const currentPage = Number(currentParams.get("page")) || 1;
        setPage(currentPage);
        setCurrentGroup(Math.ceil(currentPage / 10));
    }, [location.search]);

    if (maxPageNumber <= 1 || totalResults === 0) {
        return null;
    }

    const createPageUrl = (pageNum: number): string => {
        const searchParams = new URLSearchParams(location.search);
        searchParams.set("page", pageNum.toString()); // 페이지 번호 설정
        if (query) {
            searchParams.set("q", query); // 검색 쿼리가 있다면 설정
        }
    
        return `${location.pathname}?${searchParams.toString()}`;
    };
    
    

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
        <div className={ClassNameHelper.concat('pagination center-layout', className)}>
            {maxPageNumber > 10 && (
                <LazyCaretLeftSolidIcon onClick={handlePrevBtn}
                    className={ClassNameHelper.concat('left', currentGroup === 1 && 'disable')}
                    style={{ width: 12, height: 14 }}
                />
            )}
            {Array.from({ length: maxPageNumber }, (_, i) => i + 1)
                .slice(groupStartIndex, groupEndIndex)
                .map((num) => (
                    <PageLink
                        key={num}
                        no={num}
                        base={createPageUrl(num)}
                        className={page === num ? "active" : ''}
                        onClick={() => handlePageClick(num)}
                    />
                ))
            }
            {maxPageNumber > 10 && (
                <LazyCaretRightSolidIcon onClick={handleNextBtn}
                    className={ClassNameHelper.concat('', currentGroup === Math.ceil(maxPageNumber / 10) && 'disable')}
                    style={{ width: 12, height: 14 }}
                />
            )}
        </div>
    );
}


export default React.memo(Pagination);
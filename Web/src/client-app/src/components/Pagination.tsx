import React, { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ClassNameHelper from "../ClassNameHelper";
import PageLink from "./PageLink";
import LazyCaretRightSolidIcon from "../svgs/LazyCaretRightSolidIcon";
import LazyCaretLeftSolidIcon from "../svgs/LazyCaretLeftSolidIcon";
import LazyCaretDoubleLeftIcon from "../svgs/LazyCaretDoubleLeftIcon";
import LazyCaretDoubleRightIcon from "../svgs/LazyCaretDoubleRightIcon";

interface IPaginationProps {
    totalResults: number;
    resultsPerPage?: number;
    className?: string;
}

function Pagination({ totalResults, resultsPerPage = 10, className }: IPaginationProps) {
    const location = useLocation();
    const navigate = useNavigate();
    const currentParams = new URLSearchParams(location.search);
    const initialCurrentPage = Number(currentParams.get("page")) || 1;
    const [page, setPage] = useState(initialCurrentPage);

    const pagesPerGroup = 10;
    const currentGroup = Math.ceil(page / pagesPerGroup);
    const maxPageNumber = Math.ceil(totalResults / resultsPerPage);
    const startPage = (currentGroup - 1) * pagesPerGroup + 1;
    const endPage = Math.min(startPage + pagesPerGroup - 1, maxPageNumber);

    useEffect(() => {
        setPage(initialCurrentPage);
    }, [location.search, initialCurrentPage]);

    const handlePageChange = (newPage: number) => {
        const searchParams = new URLSearchParams(location.search);
        searchParams.set("page", String(newPage));
    
        if (currentParams.has('q')) {
            const qValue = currentParams.get('q') ?? '';
            searchParams.set("q", qValue);
        }
    
        navigate(`${location.pathname}?${searchParams.toString()}`, { replace: true });
    };
    

    const handleNextGroup = () => {
        const nextPage = Math.min(page + pagesPerGroup, maxPageNumber);
        handlePageChange(nextPage);
    };
    
    const handlePrevGroup = () => {
        const prevPage = Math.max(page - pagesPerGroup, 1);
        handlePageChange(prevPage);
    };
    

    return (
        <div className={ClassNameHelper.concat('pagination center-layout', className)}>
            {currentGroup > 1 && (
                <LazyCaretDoubleLeftIcon onClick={handlePrevGroup} />
            )}
            {page > 1 && (
                <LazyCaretLeftSolidIcon
                    onClick={() => handlePageChange(page - 1)}
                    className="pagination-icon"
                />
            )}
            {Array.from({ length: endPage - startPage + 1 }, (_, i) => startPage + i)
                .map((num) => (
                    page === num ? (
                        <div key={num} className="page-item active">
                            {num}
                        </div>
                    ) : (
                        <PageLink
                            key={num}
                            no={num}
                            base={`${location.pathname}?page=${num}`}
                            className="page-link"
                            onClick={() => handlePageChange(num)}
                        />
                    )
                ))
            }
            {page < maxPageNumber && (
                <LazyCaretRightSolidIcon
                    onClick={() => handlePageChange(page + 1)}
                    className="pagination-icon"
                />
            )}
            {currentGroup < Math.ceil(maxPageNumber / pagesPerGroup) && (
                <LazyCaretDoubleRightIcon onClick={handleNextGroup} />
            )}
        </div>
    );
}

export default React.memo(Pagination);

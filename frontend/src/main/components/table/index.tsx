import React, { useState, useCallback, useEffect, useRef } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faPlus,
  faEdit,
  faTrashCan,
  faSearch,
  faCog,
  faCheck,
} from "@fortawesome/free-solid-svg-icons";
import {
  Nav,
  Card,
  Button,
  Table,
  Dropdown,
  Pagination,
  Row,
  InputGroup,
  Form,
  Col,
} from "@themesberg/react-bootstrap";
import axios from "axios";

interface IGenericTable {
  controller: any;
  onEdit?: (id: number) => void;
  onDelete?: (id: number) => void;
  onAdd?: () => void;
}
interface IRow {
  row: any;
  columns: Array<any>;
}

export const GenericTable = (props: IGenericTable) => {
  const { controller, onEdit, onAdd, onDelete } = props;
  const [rows, setRows] = useState<Array<any>>();
  const [columns, setColumns] = useState<Array<any>>();
  const [pageSize, setPageSize] = useState<number>(10);
  const [pageNumber, setPageNumber] = useState<number>(0);
  const [searchValue, setSearchValue] = useState<string>("");
  const [hasNext, setHasNext] = useState();
  const [hasPrevious, setHasPrevious] = useState();
  const [rowCount, setRowCount] = useState(0);

  const fetchTableData = useCallback(async () => {
    const result: any = await axios.post(`${controller}/get-all`, {
      pageNumber: pageNumber,
      pageSize: pageSize,
      searchValue: searchValue,
    });

    if (result.data) {
      setRows(result.data.rows);
      setColumns(result.data.columns);
      setHasNext(result.data.hasNext);
      setHasPrevious(result.data.hasPrevious);
      setRowCount(result.data.rowCount);
    }
  }, [pageNumber, pageSize, searchValue]);

  useEffect(() => {
    fetchTableData();
  }, [pageSize, pageNumber, searchValue]);

  useEffect(() => {
    document.addEventListener(`refreshTable${controller}`, fetchTableData);
    return () => {
      document.removeEventListener(`refreshTable${controller}`, fetchTableData);
    };
  }, []);

  const TableRow = (props: IRow) => {
    const { row, columns } = props;

    return (
      <tr>
        {columns
          ?.filter((c: any) => !c.hide)
          .map((c: any, index: number) => {
            return (
              <td key={`cell${index}`}>
                <span className="fw-normal">{row[c.field]}</span>
              </td>
            );
          })}
        {(onEdit || onDelete) && (
          <td
            className="d-flex justify-content-center gap-3"
            style={{
              position: "sticky",
              right: 0,
              zIndex: 5,
              backgroundColor: "white",
            }}
          >
            {onEdit && (
              <FontAwesomeIcon
                icon={faEdit}
                size="lg"
                onClick={() => onEdit(row.id)}
                style={{ cursor: "pointer" }}
              />
            )}
            {onDelete && (
              <FontAwesomeIcon
                icon={faTrashCan}
                size="lg"
                color="red"
                onClick={() => onDelete(row.id)}
                style={{ cursor: "pointer" }}
              />
            )}
          </td>
        )}
      </tr>
    );
  };

  return (
    <div className="px-4">
      <div className="table-settings mb-4">
        <Row className="justify-content-between align-items-center">
          <Col xs={8} md={6} lg={3} xl={4} className="d-flex gap-3">
            <InputGroup>
              <InputGroup.Text>
                <FontAwesomeIcon icon={faSearch} />
              </InputGroup.Text>
              <Form.Control
                defaultValue=""
                type="text"
                placeholder="Search"
                onChange={(e: any) => setSearchValue(e.target.value)}
              />
            </InputGroup>
            {onAdd && (
              <button className="btn btn-primary" onClick={onAdd}>
                <FontAwesomeIcon icon={faPlus} />
              </button>
            )}
          </Col>
          <Col xs={4} md={2} xl={1} className="ps-md-0 text-end">
            <Dropdown>
              <Dropdown.Toggle
                split
                as={Button}
                variant="link"
                className="text-dark m-0 p-0"
              >
                <span className="icon icon-sm icon-gray">
                  <FontAwesomeIcon icon={faCog} />
                </span>
              </Dropdown.Toggle>
              <Dropdown.Menu className="dropdown-menu-xs dropdown-menu-center">
                <Dropdown.Item className="fw-bold text-dark">
                  Shfaq
                </Dropdown.Item>
                <Dropdown.Item
                  className="d-flex fw-bold"
                  onClick={() => setPageSize(5)}
                >
                  5
                  <span className="icon icon-small ms-auto">
                    <FontAwesomeIcon icon={faCheck} />
                  </span>
                </Dropdown.Item>
                <Dropdown.Item
                  className="fw-bold"
                  onClick={() => setPageSize(10)}
                >
                  10
                </Dropdown.Item>
                <Dropdown.Item
                  className="fw-bold"
                  onClick={() => setPageSize(20)}
                >
                  20
                </Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          </Col>
        </Row>
      </div>
      {columns && rows ? (
        <Card
          border="light"
          className="table-wrapper table-responsive shadow-sm"
        >
          <Card.Body className="pt-0">
            <Table hover className="user-table align-items-center">
              <thead>
                <tr>
                  {columns
                    .filter((column: any) => !column.hide)
                    .map((column: any, index: number) => {
                      return (
                        <th
                          key={`columnHeader${index}`}
                          className="border-bottom"
                        >
                          {column.headerName}
                        </th>
                      );
                    })}
                  {(onEdit || onDelete) && (
                    <th
                      className="position-sticky"
                      style={{
                        position: "sticky",
                        right: 0,
                        zIndex: 5,
                        backgroundColor: "white",
                      }}
                    >
                      Aksionet
                    </th>
                  )}
                </tr>
              </thead>
              <tbody>
                {rows.length > 0 &&
                  rows?.map((r: any, index: number) => (
                    <TableRow key={`row${index}`} row={r} columns={columns} />
                  ))}
              </tbody>
            </Table>
            {rows.length == 0 && (
              <div
                className="position-relative w-100 "
                style={{ height: "100px" }}
              >
                <div
                  className="alert alert-warning position-absolute start-50 top-50"
                  role="alert"
                >
                  Nuk ka rekorde
                </div>
              </div>
            )}
            <Card.Footer className="px-3 border-0 d-lg-flex align-items-center justify-content-between">
              <Nav>
                <Pagination className="mb-2 mb-lg-0">
                  <Pagination.Prev
                    disabled={!hasPrevious}
                    onClick={() => setPageNumber(pageNumber - 1)}
                  >
                    Pas
                  </Pagination.Prev>
                  <Pagination.Item active={pageNumber == 0}>
                    {pageNumber > 2 ? pageNumber - 2 : 1}
                  </Pagination.Item>
                  <Pagination.Item
                    active={pageNumber == 1}
                    disabled={pageNumber == 0 && hasNext == false}
                  >
                    {pageNumber > 2 ? pageNumber - 1 : 2}
                  </Pagination.Item>
                  <Pagination.Item active={pageNumber > 3} disabled>
                    {pageNumber > 2 ? pageNumber + 1 : 3}
                  </Pagination.Item>
                  <Pagination.Item>...</Pagination.Item>
                  <Pagination.Next
                    disabled={!hasNext}
                    onClick={() => setPageNumber(pageNumber + 1)}
                  >
                    Para
                  </Pagination.Next>
                </Pagination>
              </Nav>
              <small className="fw-bold">
                Shfaqur <b>{rows.length}</b> nga <b>{rowCount}</b> rekorde
              </small>
            </Card.Footer>
          </Card.Body>
        </Card>
      ) : (
        <div className="d-flex justify-content-center">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      )}
    </div>
  );
};
